using Domain.Model;
using Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDomain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
              var context1 = Infrastructure.ServiceLocator.Instance.GetService<Domain.Repositories.IRepositoryContext>();
            Domain.Repositories.IRepositoryContext context = new Repository.EntityFramework.EntityFrameworkRepositoryContext();
            EntityFrameworkRepository<Project> project = new EntityFrameworkRepository<Project>(context);

            project.Add(new Project()
            {
                ProjectNo = Guid.NewGuid().ToString(),
                Name = "小船儿轻轻"
            });

            project.Context.Commit();
        }
    }
}
