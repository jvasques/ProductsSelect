using System.Collections.Generic;

namespace ProductsSelect
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage(List<Produto> produtos)
        {
            InitializeComponent();
            ProductListView.ItemsSource = produtos;
        }
    }
}
