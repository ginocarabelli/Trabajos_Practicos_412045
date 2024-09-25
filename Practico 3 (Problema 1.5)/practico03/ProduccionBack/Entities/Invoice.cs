using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public PaymentForm PForm { get; set; }
        public string Client { get; set; }

        public List<InvoiceDetail> details;
        public Invoice()
        {
            details = new List<InvoiceDetail>();
        }
        public List<InvoiceDetail> GetDetails()
        {
            return details;
        }
        public void AddDetail(InvoiceDetail detail)
        {
            if (detail != null)
                details.Add(detail);
        }

        public void Remove(int index)
        {
            details.RemoveAt(index);
        }
        public override string ToString()
        {
            return $"[Nro: {InvoiceId}, Fecha: {InvoiceDate}, Forma de Pago: {PForm.PaymentFormName}, Cliente: {Client}]";
        }
    }
}
