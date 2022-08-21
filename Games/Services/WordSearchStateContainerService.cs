using Games.WordSearch;

namespace Games.Services
{
    public class WordSearchStateContainerService
    {
        /// <summary>
        /// The State property with initial value
        /// </summary>
        public Search Value { get; set; } = new Search(new List<string>(), 10, 10);
        /// <summary>
        /// The event that will be raised for state changed
        /// </summary>
        public event Action OnStateChange;

        /// <summary>
        /// The method that will be accessed by the sender component 
        /// to update the state
        /// </summary>
        public void SetValue(Search value)
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
