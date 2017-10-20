using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SolidHttp.Extensions.Testing.MSTest
{
    public class MSTestAsserter : IAsserter
    {
        public void AreEqual(object expected, object actual, string message)
        {
            Assert.AreEqual(expected, actual, message);
        }

        public void AreEqual<T>(T expected, T actual, string message)
        {
            Assert.AreEqual(expected, actual, message);
        }

        public void AreNotEqual<T>(T notExpected, T actual, string message)
        {
            throw new NotImplementedException();
        }

        public void AreNotEqual(object notExpected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void AreNotSame<T>(T notExpected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void AreNotSame(object notExpected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void AreSame<T>(T expected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void AreSame(object expected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void Fail(string message)
        {
            throw new NotImplementedException();
        }

        public void Inconclusive(string message)
        {
            throw new NotImplementedException();
        }

        public void IsFalse(bool value, string message)
        {
            throw new NotImplementedException();
        }

        public void IsInstanceOfType<TExpected>(object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void IsInstanceOfType(Type expected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void IsNotInstanceOfType<TNotExpected>(object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void IsNotInstanceOfType(Type notExpected, object actual, string message)
        {
            throw new NotImplementedException();
        }

        public void IsNotNull(object value, string message)
        {
            throw new NotImplementedException();
        }

        public void IsNull(object value, string message)
        {
            throw new NotImplementedException();
        }

        public void IsTrue(bool value, string message)
        {
            Assert.IsTrue(value, message);


        }
    }
}
