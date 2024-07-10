namespace Cross
{
    public class CharCell
    {
        public char Character { get; set; } = '\0';
        public bool IsOccupied => Character != '\0';
    }
}
