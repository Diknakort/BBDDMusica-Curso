namespace BaseDatosMusica.Services.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public bool IsValid(T elemento);
    }
}
