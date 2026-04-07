// ============================================================
//  FRAGMENTO DE CÓDIGO HEREDADO — MÓDULO 3
//  Archivo: PedidoService_legacy.cs
//
//  Instrucciones:
//  Lee este código, identifica los problemas que encuentres
//  y corrígelos. No es necesario arreglarlo todo — prioriza
//  los más importantes y explica brevemente tus cambios.
// ============================================================

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GestionPedidos
{
    public class PedidoService
    {
        private readonly string _connectionString;

        public PedidoService()
        {
            // CORRECCIÓN 1: Leer cadena de conexión desde configuración
            _connectionString = ConfigurationManager.ConnectionStrings["JCP2DB"].ConnectionString;
        }

        public void GuardarPedido(string clienteNombre, string totalTexto)
        {
            if (string.IsNullOrWhiteSpace(clienteNombre))
                throw new ArgumentException("El nombre del cliente es requerido");

            if (!decimal.TryParse(totalTexto, out decimal total))
                throw new ArgumentException("El formato del total es inválido");

            // CORRECCIÓN 3: Usar parámetros en lugar de concatenación
            string query = @"INSERT INTO Pedidos (ClienteNombre, Total, Fecha) 
                            VALUES (@ClienteNombre, @Total, GETDATE())";

            // CORRECCIÓN 2: using para disposición automática de recursos
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClienteNombre", clienteNombre);
                cmd.Parameters.AddWithValue("@Total", total);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarPedido(int pedidoId)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("ID de pedido inválido");

            string query = "DELETE FROM Pedidos WHERE Id = @PedidoId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // CORRECCIÓN 6: Manejo específico de errores con logging
        public decimal ObtenerTotalCliente(int clienteId)
        {
            if (clienteId <= 0)
                return 0;  // ID inválido, retornar 0

            try
            {
                string query = "SELECT SUM(Total) FROM Pedidos WHERE ClienteId = @ClienteId";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", clienteId);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }
            }
            catch (SqlException ex)
            {
                // Loggear el error específico de SQL
                Console.WriteLine($"Error de base de datos: {ex.Message}");
                throw new InvalidOperationException("Error al obtener el total del cliente", ex);
            }
            catch (Exception ex)
            {
                // Loggear otros errores
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }
    }
}