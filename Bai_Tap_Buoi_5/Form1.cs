using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai_Tap_Buoi_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho ComboBox
            toolStripComboBox1.Text = "Tahoma"; // Font mặc định là Tahoma
            toolStripComboBox2.Text = "14"; // Size mặc định là 14

            // Thêm các font chữ hệ thống vào ComboBox toolStripComboBox1
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }

            // Thêm các kích thước chữ từ 8 đến 72 vào ComboBox toolStripComboBox2
            List<int> listSize = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (var s in listSize)
            {
                toolStripComboBox2.Items.Add(s);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            // Khi click vào ComboBox font, thay đổi font cho RichTextBox
            richText.Font = new Font(toolStripComboBox1.SelectedItem.ToString(), richText.Font.Size);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Xóa nội dung hiện có trên RichTextBox
            richText.Clear();

            // Khôi phục font và size mặc định
            richText.Font = new Font("Tahoma", 14); // Tạo font mặc định là Tahoma, size 14
            richText.ForeColor = Color.Black; // Đặt màu chữ mặc định là màu đen

            // Cập nhật lại giá trị trên các ComboBox nếu cần
            toolStripComboBox1.Text = "Tahoma";
            toolStripComboBox2.Text = "14";
        }

        private bool isFileSaved = true; // Biến để theo dõi trạng thái lưu file
        private string currentFilePath = ""; // Lưu đường dẫn của tập tin hiện tại
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Nếu là văn bản mới và chưa lưu
            if (isFileSaved)
            {
                // Hiển thị hộp thoại SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf"; // Chỉ cho phép lưu file *.rtf
                saveFileDialog.Title = "Lưu Tập Tin Văn Bản";

                // Nếu người dùng chọn một tập tin để lưu
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Lưu nội dung RichTextBox vào file
                        currentFilePath = saveFileDialog.FileName; // Lưu đường dẫn tập tin
                        richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText); // Lưu dưới định dạng RTF
                        isFileSaved = true; // Đánh dấu văn bản đã lưu
                        MessageBox.Show("Văn bản đã được lưu thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu tập tin: " + ex.Message);
                    }
                }
            }
            else
            {
                // Nếu đã lưu trước đó, thông báo cho người dùng
                MessageBox.Show("Văn bản đã được lưu trước đó!");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có văn bản được chọn hay không
            if (richText.SelectionLength > 0)
            {
                // Kiểm tra xem văn bản đã được in đậm hay chưa
                if (richText.SelectionFont.Bold)
                {
                    // Nếu đã in đậm, bỏ in đậm
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Bold);
                }
                else
                {
                    // Nếu chưa in đậm, áp dụng in đậm
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Bold);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có văn bản được chọn hay không
            if (richText.SelectionLength > 0)
            {
                // Kiểm tra xem văn bản đã được in nghiêng hay chưa
                if (richText.SelectionFont.Italic)
                {
                    // Nếu đã in nghiêng, bỏ in nghiêng
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Italic);
                }
                else
                {
                    // Nếu chưa in nghiêng, áp dụng in nghiêng
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Italic);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Khởi tạo hộp thoại OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Cấu hình hộp thoại chỉ cho phép chọn tập tin .txt và .rtf
            openFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
            openFileDialog.Title = "Mở Tập Tin Văn Bản";

            // Hiển thị hộp thoại và kiểm tra nếu người dùng chọn một tập tin hợp lệ
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Đọc và mở nội dung tập tin vào RichTextBox
                    string filePath = openFileDialog.FileName;

                    // Kiểm tra phần mở rộng của tập tin để mở tương ứng
                    if (filePath.EndsWith(".txt"))
                    {
                        richText.LoadFile(filePath, RichTextBoxStreamType.PlainText); // Mở .txt
                    }
                    else if (filePath.EndsWith(".rtf"))
                    {
                        richText.LoadFile(filePath, RichTextBoxStreamType.RichText); // Mở .rtf
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở tập tin: " + ex.Message);
                }
            }
        }

        private void richText_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có văn bản được chọn hay không
            if (richText.SelectionLength > 0)
            {
                // Kiểm tra xem văn bản đã được gạch dưới hay chưa
                if (richText.SelectionFont.Underline)
                {
                    // Nếu đã gạch dưới, bỏ gạch dưới
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Underline);
                }
                else
                {
                    // Nếu chưa gạch dưới, áp dụng gạch dưới
                    richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Underline);
                }
            }
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            // Khi click vào ComboBox size, thay đổi size cho RichTextBox
            richText.Font = new Font(richText.Font.FontFamily, Convert.ToInt32(toolStripComboBox2.SelectedItem.ToString()));
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }
    }
}
