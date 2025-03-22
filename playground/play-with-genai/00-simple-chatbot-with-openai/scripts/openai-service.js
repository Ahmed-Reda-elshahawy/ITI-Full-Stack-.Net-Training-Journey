export class OpenAiService {
  _endpoints;
  _apiKey;
  _promptGuide;
  _model;
  _history;
  _maxHistory;

  constructor(endpoints, apiKey, maxHistory = 25) {
    this._endpoints = endpoints;
    this._apiKey = apiKey;
    this._promptGuide =
      "Keep your response in simple and easy English words, make your response short and clear in points and write your response in HTML as div and style it with Bootstrap!";
    this._model = "gpt-4o-mini";
    this._history = [];
    this._maxHistory = maxHistory;
  }

  set model(value) {
    if (!this._endpoints[value]) {
      throw new Error(`No endpoint defined for model: ${value}`);
    }
    this._model = value;
  }

  _addToHistory(role, content) {
    this._history.push({ role, content });
    if (this._history.length > this._maxHistory) {
      this._history.shift();
    }
  }

  clearHistory() {
    this._history = [];
  }

  _isImageModel() {
    return this._model.toLowerCase().includes("dall-e");
  }

  async _sendRequest(body) {
    const endpoint = this._endpoints[this._model];
    if (!endpoint) {
      throw new Error(`No endpoint found for model: ${this._model}`);
    }

    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${this._apiKey}`,
      },
      body,
    });

    if (!response.ok) {
      throw new Error(`API error: ${response.status} - ${response.statusText}`);
    }

    return await response.json();
  }

  _getTextRequestBody(prompt, promptGuide) {
    const messages = [
      { role: "system", content: promptGuide ?? this._promptGuide },
      ...this._history,
    ];
    return JSON.stringify({ model: this._model, messages });
  }

  _getImageRequestBody(prompt) {
    return JSON.stringify({ model: this._model, prompt, size: "1024x1024" });
  }

  async _completeTextGeneration(prompt, promptGuide = undefined) {
    const responseData = await this._sendRequest(
      this._getTextRequestBody(prompt, promptGuide)
    );

    const content =
      responseData.choices?.[0]?.message?.content || "No response received.";

    return `<div class="p-2">${content}</div>`; // Wrap in div for consistency
  }

  async _completeImageGeneration(prompt) {
    const responseData = await this._sendRequest(
      this._getImageRequestBody(prompt)
    );

    const imageUrl = responseData.data?.[0]?.url || "No image generated.";

    return `
    <div class="d-flex justify-content-end">
      <img src="${imageUrl}" alt="Generated Image" class="img-fluid rounded shadow-lg mx-auto" 
           style="width:50%; object-fit:contain;" />
    </div>
  `;
  }

  async getCompletion(prompt, promptGuide = undefined) {
    try {
      this._addToHistory("user", prompt);

      const botResponse = await (this._isImageModel()
        ? this._completeImageGeneration(prompt)
        : this._completeTextGeneration(prompt, promptGuide));

      this._addToHistory("assistant", botResponse);

      return botResponse;
    } catch (error) {
      console.error("Error fetching OpenAI response:", error);
      return this._isImageModel()
        ? '<div class="alert alert-danger">Sorry, failed to generate image.</div>'
        : '<div class="alert alert-danger">Sorry, something went wrong. Please try again.</div>';
    }
  }
}
