namespace FoxoLib
{
    public class SaveFileException : Exception
    {
        public SaveFileException() : base("Hiba történt a slot betöltésekor, lehet, hogy a mentés meghibásodott!") { }
    }
}