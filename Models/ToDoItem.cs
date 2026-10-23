namespace Models
{
    public class ToDoItem
    {
            public string Title { get; set; }

            public bool IsCompleted { get; set; }

        public ToDoItem(string title, bool isCompleted)
        {
            Title = title;
            IsCompleted = isCompleted;
        }

    }
}
