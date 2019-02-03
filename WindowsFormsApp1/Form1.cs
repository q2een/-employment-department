using SharpUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form, ISharpUpdatable
    {
        SharpUpdater updater;

        public Form1()
        {
            InitializeComponent();
            updater = new SharpUpdater(this);
        }

        #region ISharpUpdatable

        /// <summary>
        /// Наименование приложения.
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return "выа";
            }
        }

        /// <summary>
        /// ID приложения
        /// </summary>
        public string ApplicationID
        {
            get
            {
                return "_";
            }
        }

        /// <summary>
        /// Сборка.
        /// </summary>
        public Assembly ApplicationAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        /// <summary>
        /// Значок.
        /// </summary>
        public Icon ApplicationIcon
        {
            get
            {
                return this.Icon;
            }
        }

        /// <summary>
        /// URL для обновления.
        /// </summary>
        public Uri UpdateXmlLocation
        {
            get
            {
                return new Uri("file:///E:/Downloads/project1.xml");
            }
        }

        /// <summary>
        /// Контекст.
        /// </summary>
        public Form Context
        {
            get
            {
                return this;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            updater.DoUpdate();
        }
    }
}
