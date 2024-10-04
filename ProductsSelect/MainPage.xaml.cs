using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Net;

namespace ProductsSelect
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string host = HostEntry.Text;
            string port = PortEntry.Text;
            string database = DatabaseEntry.Text;
            string user = UserEntry.Text;
            string password = PasswordEntry.Text;

            string connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";

            try
            {
                List<Produto> produtos = await GetProductsAsync(connectionString);
                // Navegar para a nova página e passar a lista de produtos
                await Navigation.PushAsync(new ProductListPage(produtos));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }


        private async Task<List<Produto>> GetProductsAsync(string connectionString)
        {
            var produtos = new List<Produto>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("SELECT produto_codigo, produto_descricao FROM produto order by produto_codigo", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produto = new Produto
                        {
                            produto_codigo = reader.GetInt64(0), // Alterado para GetInt64
                            produto_descricao = reader.GetString(1)
                        };
                        produtos.Add(produto);
                    }
                }
            }
            return produtos;
        }
    }

    public class Produto
    {
        public long produto_codigo { get; set; } // Alterado para long
        public string? produto_descricao { get; set; }
    }
}
