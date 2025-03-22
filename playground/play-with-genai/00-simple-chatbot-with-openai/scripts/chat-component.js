export class ChatComponent {
  constructor(containerRef) {
    this._containerRef = containerRef;
  }

  appendMessageContainer() {
    const messageContainer = document.createElement("div");
    messageContainer.classList.add("message-container");
    this._containerRef.appendChild(messageContainer);
    return messageContainer;
  }

  addMessage(message, role, container) {
    const messageDiv = document.createElement("div");

    messageDiv.classList.add(
      "message",
      role === "user" ? "user-message" : "bot-message"
    );

    if (role === "bot") {
      messageDiv.innerHTML = message; // Render AI-generated HTML
    } else {
      messageDiv.textContent = message; // Keep user input as plain text
    }

    container.appendChild(messageDiv);
    this._containerRef.scrollTop = this._containerRef.scrollHeight;

    return messageDiv;
  }
}
