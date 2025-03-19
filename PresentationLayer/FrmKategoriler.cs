using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FrmKategoriler : Form
    {
        private readonly ICategoryService _categoryService;
        public FrmKategoriler()
        {
            InitializeComponent();
            _categoryService = new CategoryManager(new EfCategoryDal());
        }

        private void FrmKategoriler_Load(object sender, EventArgs e)
        {

        }

        private void listCategories()
        {
            var values = _categoryService.TGetAll();
            dataGridView1.DataSource = values;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category
            {
                CategoryName = txtName.Text,
            };

            try
            {
                _categoryService.TInsert(category);
                MessageBox.Show("Kategori eklendi");
                listCategories();
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Doğrulama hatalarını göster
                foreach (var failure in ex.Errors)
                {
                    MessageBox.Show(failure.ErrorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // ID kontrolü
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Geçerli bir ID giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var deletedValue = _categoryService.TGetById(id);
            if (deletedValue == null)
            {
                MessageBox.Show("Böyle bir kategori bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _categoryService.TDelete(deletedValue);
                MessageBox.Show("Kategori silindi");
                listCategories();
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Doğrulama hatalarını göster
                foreach (var failure in ex.Errors)
                {
                    MessageBox.Show(failure.ErrorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ID kontrolü
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Geçerli bir ID giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var updatedValue = _categoryService.TGetById(id);
            if (updatedValue == null)
            {
                MessageBox.Show("Böyle bir kategori bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            updatedValue.CategoryName = txtName.Text;

            try
            {
                _categoryService.TUpdate(updatedValue);
                MessageBox.Show("Kategori güncellendi");
                listCategories();
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Doğrulama hatalarını göster
                foreach (var failure in ex.Errors)
                {
                    MessageBox.Show(failure.ErrorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnKitaplar_Click(object sender, EventArgs e)
        {
            FrmKitaplar frmKitaplar = new FrmKitaplar();
            frmKitaplar.Show();
            this.Hide();
        }

        private void btnYazarlar_Click(object sender, EventArgs e)
        {
            FrmYazarlar frmYazarlar = new FrmYazarlar();
            frmYazarlar.Show();
            this.Hide();
        }
    }
}
