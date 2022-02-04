using Editor.UI.DataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public static class LogViewerHelper
    {
        public static void CreateTabPage(string title, out TabPage tabPage, out DataGridView gridView)
        {
            gridView = CreateGridView();
            tabPage = new TabPage();
            tabPage.Controls.Add(gridView);
            tabPage.Location = new System.Drawing.Point(4, 24);
            tabPage.Name = title;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(911, 502);
            tabPage.TabIndex = 0;
            tabPage.Text = title;
            tabPage.UseVisualStyleBackColor = true;
        }

        private static DataGridView CreateGridView()
        {
            DataGridView workspaceGridView = new DataGridView();
            DataGridViewImageColumn logLevelColumn = new DataGridViewImageColumn();
            DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
            // 
            // logLevelColumn
            // 
            logLevelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            logLevelColumn.DataPropertyName = "LogLevel";
            logLevelColumn.HeaderText = "";
            logLevelColumn.Name = "logLevelColumn";
            logLevelColumn.ReadOnly = true;
            logLevelColumn.Width = 5;
            logLevelColumn.CellTemplate = new LogLevelCell();
            // 
            // messageColumn
            // 
            messageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            messageColumn.DataPropertyName = "Line";
            messageColumn.HeaderText = "Line";
            messageColumn.Name = "messageColumn";
            messageColumn.ReadOnly = true;
            messageColumn.CellTemplate = new LogTextCell();
            // 
            // workspaceGridView
            // 
            workspaceGridView.AllowUserToAddRows = false;
            workspaceGridView.AllowUserToDeleteRows = false;
            workspaceGridView.AllowUserToOrderColumns = true;
            workspaceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            workspaceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            logLevelColumn,
            messageColumn});
            workspaceGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            workspaceGridView.Location = new System.Drawing.Point(3, 3);
            workspaceGridView.Name = "workspaceGridView";
            workspaceGridView.ReadOnly = true;
            workspaceGridView.RowTemplate.Height = 20;
            workspaceGridView.Size = new System.Drawing.Size(905, 496);
            workspaceGridView.TabIndex = 1;
            workspaceGridView.AutoGenerateColumns = false;

            return workspaceGridView;
        }
    }
}
