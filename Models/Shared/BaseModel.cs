namespace UTAD.ToDoList.WPF.Models.Shared
{
    public abstract class BaseModel
    {
        public string Id { get; set; }

        protected BaseModel()
        {
            if(string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }
    }
}
