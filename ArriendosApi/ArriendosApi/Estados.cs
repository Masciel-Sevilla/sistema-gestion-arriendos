namespace ArriendosApi
{
    public static class Estados
    {
        public static class Contrato
        {
            public const string Vigente = "VIGENTE";
            public const string Finalizado = "FINALIZADO";
            public const string Cancelado = "CANCELADO";

            public static readonly string[] Todos = { Vigente, Cancelado, Finalizado };
            public static bool EsValido(string estado)=>Todos.Contains(estado);
        }

        public static class Inmueble
        {
            public const string Disponible = "DISPONIBLE";
            public const string Ocupado = "OCUPADO";
            public const string Mantenimiento = "MANTENIMIENTO";
            public const string Inactivo = "INACTIVO";
           
            public static readonly string[] Todos = { Disponible, Ocupado, Mantenimiento, Inactivo };
            public static bool EsValido(string estado) => Todos.Contains(estado);

        }
        public static class Cobro
        {
            public const string Pendiente = "PENDIENTE";
            public const string Pagado = "PAGADO";
            public const string Parcial = "PARCIAL";
            public const string Anulado = "ANULADO";

            public static readonly string[] Todos = { Pendiente, Pagado, Parcial, Anulado };
            public static bool EsValido(string estado) => Todos.Contains(estado);

        }

    }
}
