namespace BibliotecaApi
{
    public class ServicioTransient 
    {
        private readonly Guid id;

        public ServicioTransient()
        {
            id = Guid.NewGuid();
        }

        public Guid obtenerGuid => id;
    }

    public class ServicioScoped
    {
        private readonly Guid id;

        public ServicioScoped()
        {
            id = Guid.NewGuid();
        }

        public Guid obtenerGuid => id;
    }

    public class ServicioSingleton
    {
        private readonly Guid id;

        public ServicioSingleton()
        {
            id = Guid.NewGuid();
        }

        public Guid obtenerGuid => id;
    }
}
