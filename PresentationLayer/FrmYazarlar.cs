using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
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
    public partial class FrmYazarlar : Form
    {

        private readonly IAuthorService _authorService;

        public FrmYazarlar()
        {
            InitializeComponent();
            _authorService = new AuthorManager(new EfAuthorDal());
        }

        private void FrmYazarlar_Load(object sender, EventArgs e)
        {
        }

        private void ListAuthors()
        {
            var values = _authorService.TGetAll();
            dataGridView1.DataSource = values;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListAuthors();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Author author = new Author
            {
                AuthorName = txtName.Text,
                AuthorLastname = txtLastname.Text,
            };

            try
            {
                _authorService.TInsert(author);
                MessageBox.Show("Yazar eklendi.");
                ListAuthors();
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

            // Yazarın varlığını kontrol etme
            var authorToDelete = _authorService.TGetById(id);
            if (authorToDelete == null)
            {
                MessageBox.Show("Böyle bir yazar bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _authorService.TDelete(authorToDelete);
                MessageBox.Show("Yazar silindi.");
                ListAuthors();
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

            // Yazar bilgilerini güncelleme
            var authorToUpdate = _authorService.TGetById(id);
            if (authorToUpdate == null)
            {
                MessageBox.Show("Böyle bir yazar bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            authorToUpdate.AuthorName = txtName.Text;
            authorToUpdate.AuthorLastname = txtLastname.Text;

            try
            {
                _authorService.TUpdate(authorToUpdate);
                MessageBox.Show("Yazar güncellendi.");
                ListAuthors();
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

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            FrmKategoriler frmKategoriler = new FrmKategoriler();
            frmKategoriler.Show();
            this.Hide();
        }

        private void btnKitaplar_Click(object sender, EventArgs e)
        {
            FrmKitaplar frmKitaplar = new FrmKitaplar();
            frmKitaplar.Show();
            this.Hide();
        }
    }
}
