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
    public partial class FrmKitaplar : Form
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;

        public FrmKitaplar()
        {
            InitializeComponent();
            _bookService = new BookManager(new EfBookDal());
            _categoryService = new CategoryManager(new EfCategoryDal());
            _authorService = new AuthorManager(new EfAuthorDal());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboboxlar için 
            var categoryValues = _categoryService.TGetAll();
            cmbCategory.DataSource = categoryValues;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";

            var authorValues = _authorService.TGetFullName();
            cmbAuthor.DataSource = authorValues;
            cmbAuthor.DisplayMember = "FullName";
            cmbAuthor.ValueMember = "AuthorId";
        }

        private void ListBooks()
        {
            var values = _bookService.TGetBooksWithCategory();
            dataGridView1.DataSource = values;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListBooks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Book book = new Book
            {
                BookName = txtName.Text,
                BookDescription = txtDescription.Text,
                CategoryId = Convert.ToInt32(cmbCategory.SelectedValue),
                AuthorId = (int)cmbAuthor.SelectedValue,
            };

            try
            {
                _bookService.TInsert(book);
                MessageBox.Show("Kitap Eklendi");
                ListBooks();
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
            var updatedValue = _bookService.TGetById(id);
            if (updatedValue == null)
            {
                MessageBox.Show("Böyle bir kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            updatedValue.BookName = txtName.Text;
            updatedValue.BookDescription = txtDescription.Text;
            updatedValue.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            updatedValue.AuthorId = (int)cmbAuthor.SelectedValue;

            try
            {
                _bookService.TUpdate(updatedValue);
                MessageBox.Show("Kitap Güncellendi");
                ListBooks();
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
            var deletedValue = _bookService.TGetById(id);
            if (deletedValue == null)
            {
                MessageBox.Show("Böyle bir kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _bookService.TDelete(deletedValue);
                MessageBox.Show("Kitap Silindi");
                ListBooks();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnYazarlar_Click(object sender, EventArgs e)
        {
            FrmYazarlar frmYazarlar = new FrmYazarlar();
            frmYazarlar.Show();
            this.Hide();
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            FrmKategoriler frmKategoriler = new FrmKategoriler();
            frmKategoriler.Show();
            this.Hide();
        }
    }
}
