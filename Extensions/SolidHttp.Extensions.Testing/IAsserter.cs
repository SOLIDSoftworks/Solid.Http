using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public interface IAsserter
    {
        void AreEqual<T>(T expected, T actual, string message);
        void AreEqual(object expected, object actual, string message);

        void AreNotEqual<T>(T notExpected, T actual, string message);
        void AreNotEqual(object notExpected, object actual, string message);

        void AreSame<T>(T expected, object actual, string message);
        void AreSame(object expected, object actual, string message);

        void AreNotSame<T>(T notExpected, object actual, string message);
        void AreNotSame(object notExpected, object actual, string message);

        void IsInstanceOfType<TExpected>(object actual, string message);
        void IsInstanceOfType(Type expected, object actual, string message);

        void IsNotInstanceOfType<TNotExpected>(object actual, string message);
        void IsNotInstanceOfType(Type notExpected, object actual, string message);

        void IsNull(object value, string message);

        void IsNotNull(object value, string message);

        void Fail(string message);

        void Inconclusive(string message);

        void IsTrue(bool value, string message);

        void IsFalse(bool value, string message);
    }
}
