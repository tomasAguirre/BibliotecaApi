using BibliotecaApi.Entidades;

namespace BibliotecaApi
{
    public interface IRepositorioValores
    {
        public IEnumerable<Valor> obtenerValores();
    }
}
