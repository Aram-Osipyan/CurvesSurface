
namespace BezierCurve
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Point = new System.Windows.Forms.Button();
            this.Point2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Point
            // 
            this.Point.Location = new System.Drawing.Point(2, 2);
            this.Point.Margin = new System.Windows.Forms.Padding(0);
            this.Point.Name = "Point";
            this.Point.Size = new System.Drawing.Size(24, 23);
            this.Point.TabIndex = 0;
            this.Point.UseVisualStyleBackColor = true;
            // 
            // Point2
            // 
            this.Point2.BackColor = System.Drawing.Color.LightCoral;
            this.Point2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Point2.Location = new System.Drawing.Point(2, 25);
            this.Point2.Margin = new System.Windows.Forms.Padding(0);
            this.Point2.Name = "Point2";
            this.Point2.Size = new System.Drawing.Size(15, 16);
            this.Point2.TabIndex = 2;
            this.Point2.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Point2);
            this.Controls.Add(this.Point);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Point;
        private System.Windows.Forms.Button Point2;
    }
}

