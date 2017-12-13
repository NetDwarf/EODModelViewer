namespace EODModelViewer.Models
{
    public class Mob : IModelObject
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public bool IsBiped { get; set; }
        public bool IsFemale { get; set; }
        public bool IsVampiir { get; set; }
        public bool IsDemon { get; set; }
        public bool IsAnimal { get; set; }
        public int MountId { get; set; }
        public bool IsOther { get; set; }
    }
}