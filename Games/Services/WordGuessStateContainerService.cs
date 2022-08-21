using Games.WordGuess;

namespace Games.Services
{
    public class WordGuessStateContainerService
    {
        /// <summary>
        /// The State property with initial value
        /// </summary>
        public Wordle Value { get; set; } = new Wordle();
        /// <summary>
        /// The event that will be raised for state changed
        /// </summary>
        public event Action OnStateChange;

        /// <summary>
        /// The method that will be accessed by the sender component 
        /// to update the state
        /// </summary>
        public void SetValue(Wordle value)
        {
            Value = value;
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
