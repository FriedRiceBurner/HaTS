using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveDebugConsole : MonoBehaviour
{
    public bool showDebug = true;
    public static LiveDebugConsole Instance; // Singleton instance

    public TMP_Text debugText; // Reference to the Text Mesh Pro UI element
    public int maxLines = 10; // Maximum number of lines to display
    public float messageCooldown = 0.1f; // Time in seconds before a message can be displayed again

    // refernce to the hand
    // public Transform hand;

    private Queue<string> messageQueue = new Queue<string>(); // Queue to hold debug messages
    private Dictionary<string, float> lastMessageTimes = new Dictionary<string, float>(); // Dictionary to track last message times

    private void Awake()
    {
        Instance = this;
        if (showDebug)
        {
            debugText.gameObject.SetActive(true);
        }
        else
        {
            debugText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // LiveDebugConsole.Instance.Log("OVR hand rotation: " + hand.rotation.ToString());
        // Display debug messages from the queue
        if (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();
            string key = GetMessageKey(message);
            if (!IsMessageOnCooldown(key))
            {
                debugText.text += message + "\n";

                // Update last message time
                lastMessageTimes[key] = Time.time;

                // Remove oldest messages if exceeding maxLines
                if (debugText.text.Split('\n').Length > maxLines)
                {
                    RemoveOldestMessage();
                }
            }
        }
    }

    // Method to add a debug message to the queue
    public void Log(string message)
    {
        messageQueue.Enqueue(message);
    }

    // Method to get the key from the message (part before ':')
    private string GetMessageKey(string message)
    {
        int colonIndex = message.IndexOf(':');
        if (colonIndex != -1)
        {
            return message.Substring(0, colonIndex);
        }
        else
        {
            return message; // Return the entire message if no ':' found
        }
    }

    // Method to check if a message is on cooldown
    private bool IsMessageOnCooldown(string key)
    {
        if (lastMessageTimes.ContainsKey(key))
        {
            return Time.time - lastMessageTimes[key] < messageCooldown;
        }
        return false; // Message not found, not on cooldown
    }

    // Method to remove the oldest message
    private void RemoveOldestMessage()
    {
        int index = debugText.text.IndexOf("\n");
        debugText.text = debugText.text.Substring(index + 1);
    }
}