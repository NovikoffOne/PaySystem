using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            const string QiwiID = "QIWI";
            const string CardID = "Card";
            const string WebMoneyID = "WebMoney";

            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            paymentHandler.Add(new PaymentSystem(QiwiID));
            paymentHandler.Add(new PaymentSystem(CardID));
            paymentHandler.Add(new PaymentSystem(WebMoneyID));

            var systemId = orderForm.ShowForm();

            paymentHandler.ShowPaymentResult(systemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public interface IPaymentSystem
    {
        string GetSystemId();

        void ShowPaymentResult();
    }

    public class PaymentHandler
    {
        private List<IPaymentSystem> _payments;

        public PaymentHandler()
        {
            _payments = new List<IPaymentSystem>();
        }

        public void Add(IPaymentSystem paymentSystem)
        {
            _payments.Add(paymentSystem);
        }

        public void ShowPaymentResult(string systemId)
        {
            foreach (var payment in _payments)
            {
                if (payment.GetSystemId() == systemId)
                {
                    Console.WriteLine($"Перевод на страницу {payment.GetSystemId()}...");
                    payment.ShowPaymentResult();
                }
            }
        }
    }

    public class PaymentSystem : IPaymentSystem
    {
        private string _systemId;

        public PaymentSystem(string id)
        {
            _systemId = id;
        }

        public string GetSystemId()
        {
            return _systemId;
        }

        public void ShowPaymentResult()
        {
            Console.WriteLine($"Вы оплатили с помощью {_systemId}");
            Console.WriteLine($"Проверка платежа через {_systemId}...");
            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}