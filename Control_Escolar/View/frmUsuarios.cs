﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control_Escolar.Utilities;

namespace Control_Escolar.View
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios(Form parent)
        {
            InitializeComponent();
            Formas.InicializaForma(this, parent);
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
