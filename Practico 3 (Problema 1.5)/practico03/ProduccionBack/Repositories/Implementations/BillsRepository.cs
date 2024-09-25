using ProduccionBack.Domain;
using ProduccionBack.Repositories.Contracts;
using ProduccionBack.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Repositories.Implementations
{
    public class BillsRepository : IBillsRepository
    {
        public List<Invoice> GetAll()
        {
            List<Invoice> lst = new List<Invoice>();
            Invoice? oInvoice = null;
            var helper = DataHelper.GetInstance();
            DataTable t = helper.ExecuteSPQuery("SP_GetAll", null);
            foreach (DataRow row in t.Rows)
            {
                //lee la primer fila y tomar datos del maestro y primer detalle
                if (oInvoice == null || oInvoice.InvoiceId != Convert.ToInt32(row["INVOICE_ID"].ToString()))
                {
                    oInvoice = new Invoice();
                    oInvoice.InvoiceId = Convert.ToInt32(row["INVOICE_ID"].ToString());
                    oInvoice.PForm = ReadPaymentForm(row);
                    oInvoice.InvoiceDate = Convert.ToDateTime(row["INVOICE_DATE"].ToString());
                    oInvoice.Client = Convert.ToString(row["CLIENT"]);
                    oInvoice.AddDetail(ReadDetail(row));
                    lst.Add(oInvoice);
                }
                else
                {
                    //mientras no cambia el Id del maestro, leer datos de detalles.
                    oInvoice.AddDetail(ReadDetail(row));
                }
            }
            return lst;
        }
        private PaymentForm ReadPaymentForm(DataRow row)
        {
            PaymentForm paymentForm = new PaymentForm();
            paymentForm.PaymentFormId = Convert.ToInt32(row["PAYMENT_FORM_ID"]);
            paymentForm.PaymentFormName = row["PAYMENT_FORM_NAME"].ToString();
            return paymentForm;
        }
        private InvoiceDetail ReadDetail(DataRow row)
        {
            InvoiceDetail detail = new InvoiceDetail();
            detail.Article = new Article()
            {
                ArticleID = Convert.ToInt32(row["ARTICLE_ID"].ToString()),
                ArticleName = row["ARTICLE_NAME"].ToString(),
                UnitPrice = Convert.ToDouble(row["UNIT_PRICE"].ToString())
            };
            detail.Quantity = Convert.ToInt32(row["QUANTITY"]);
            return detail;
        }

        public Invoice GetInvoiceById(int id)
        {
            Invoice oInvoice = new Invoice();
            var helper = DataHelper.GetInstance();
            DataTable t = helper.ExecuteSPQuery("SP_GetInvoiceById", new SqlParameter("@ID", id));
            if (t.Rows.Count > 0)
            {
                DataRow row = t.Rows[0];

                oInvoice.InvoiceId = Convert.ToInt32(row["INVOICE_ID"]);
                oInvoice.PForm = ReadPaymentForm(row);
                oInvoice.InvoiceDate = Convert.ToDateTime(row["INVOICE_DATE"]);
                oInvoice.Client = Convert.ToString(row["CLIENT"]);
            }
            return oInvoice;
        }
        public bool Validate(int id)
        {
            var helper = DataHelper.GetInstance();
            DataTable table = helper.ExecuteSPQuery("SP_GetInvoiceDetailsById", new SqlParameter("@ID", id));
            if (table.Rows.Count > 0)
            {
                Console.WriteLine("El código de este detalle factura ya existe, ingrese otro");
                return true;
            }
            return false;
        }
        public bool Save(Invoice Invoice)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_SaveInvoice", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                //input parameters
                cmd.Parameters.AddWithValue("@ID", Invoice.InvoiceId);
                cmd.Parameters.AddWithValue("@INVOICE_DATE", Invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@PAYMENT_FORM_ID", Invoice.PForm.PaymentFormId);
                cmd.Parameters.AddWithValue("@CLIENT", Invoice.Client);
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
        public bool Update(Invoice Invoice)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_UpdateInvoice", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                //input parameters
                cmd.Parameters.AddWithValue("@ID", Invoice.InvoiceId);
                cmd.Parameters.AddWithValue("@INVOICE_DATE", Invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@PAYMENT_FORM_ID", Invoice.PForm.PaymentFormId);
                cmd.Parameters.AddWithValue("@CLIENT", Invoice.Client);

                //output parameters
                SqlParameter param = new SqlParameter("id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int invoiceId = (int)param.Value;
                int nroDetail = 1;

                foreach (var detail in Invoice.GetDetails())
                {
                    var cmdDetail = new SqlCommand("SP_UpdateInvoiceDetail", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;

                    cmdDetail.Parameters.AddWithValue("@ID", nroDetail);
                    cmdDetail.Parameters.AddWithValue("@ARTICLE_ID", detail.Article.ArticleID);
                    cmdDetail.Parameters.AddWithValue("@QUANTITY", detail.Quantity);
                    cmdDetail.Parameters.AddWithValue("@INVOICE_ID", Invoice.InvoiceId);
                    cmdDetail.ExecuteNonQuery();

                    nroDetail++;
                }
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_DeleteInvoiceDetail", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                var filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas != 0)
                {
                    var cmdInvoice = new SqlCommand("SP_DeleteInvoice", cnn, t);
                    cmdInvoice.CommandType = CommandType.StoredProcedure;
                    cmdInvoice.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }

    }
}
