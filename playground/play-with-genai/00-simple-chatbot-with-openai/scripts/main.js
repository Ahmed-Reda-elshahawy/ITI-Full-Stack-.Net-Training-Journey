import { openApiConfig } from "./openapi.config.js";
import { OpenAiService } from "./openai-service.js";
import { ChatComponent } from "./chat-component.js";

// Get elements
const userInputElement = document.getElementById("userInput");
const chatContainer = document.getElementById("chatContainer");
const submitBtn = document.getElementById("submitBtn");
const modelSelect = document.getElementById("modelSelect");

// Initialize OpenAI API handler
const openAiService = new OpenAiService(
  openApiConfig.endpoints,
  openApiConfig.apiKey
);

// Initialize chat Ui
const chatUI = new ChatComponent(chatContainer);

// Handle submit event
async function onSubmit() {
  const userMessage = userInputElement.value.trim();
  if (!userMessage) return;

  const messageContainer = chatUI.appendMessageContainer();

  // Add user message (left-aligned)
  chatUI.addMessage(userMessage, "user", messageContainer);
  userInputElement.value = ""; // Clear input

  // Add waiting message (right-aligned)
  const responseMessageDiv = chatUI.addMessage(
    "<i>Waiting for response...</i>",
    "bot",
    messageContainer
  );

  try {
    const selectedModel = modelSelect.value;
    openAiService.model = selectedModel;

    // Fetch AI response
    let response = await openAiService.getCompletion(userMessage);

    // Remove triple backticks from AI response
    const cleanResponse = response.replace(/```html|```/g, "");

    // Replace waiting message with actual response (HTML-rendered)
    responseMessageDiv.innerHTML = cleanResponse;
  } catch (error) {
    responseMessageDiv.innerHTML = `<span style="color: red;">Error fetching response. Please try again.</span>`;
  }
}

// Attach event listener
submitBtn.addEventListener("click", onSubmit);

// Allow pressing Enter to submit
userInputElement.addEventListener("keypress", (event) => {
  if (event.key === "Enter" && !event.shiftKey) {
    event.preventDefault();
    onSubmit();
  }
});
