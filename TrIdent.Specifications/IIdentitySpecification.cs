using FluentAssertions;
using System;
using System.Reflection;
using Xunit;

namespace TrIdent.Specifications
{
   public abstract class IIdentitySpecification<TIdentity>
       where TIdentity : IIdentity
   {
      #region specifications
      [Fact]
      public void Implementation__Implementations_provide_their_own_Object_Equals_implementation()
      {

         var sutType = typeof(TIdentity);
         var objectEqualsMethod = sutType.GetMethod("Equals", new[] { typeof(object) });

         objectEqualsMethod.Should().NotBeNull();
         objectEqualsMethod.Attributes.Should().HaveFlag(MethodAttributes.Public);
         objectEqualsMethod.Attributes.Should().NotHaveFlag(MethodAttributes.Static);
         objectEqualsMethod.Attributes.Should().NotHaveFlag(MethodAttributes.Abstract);
         objectEqualsMethod.DeclaringType.Should().Be(sutType);
      }

      [Fact]
      public void Implementation__Implementations_provide_their_own_Object_GetHashCode_implementation()
      {
         var sutType = typeof(TIdentity);
         var getHashCodeMethod = sutType.GetMethod("GetHashCode", new Type[0]);

         getHashCodeMethod.Should().NotBeNull();
         getHashCodeMethod.Attributes.Should().HaveFlag(MethodAttributes.Public);
         getHashCodeMethod.Attributes.Should().NotHaveFlag(MethodAttributes.Static);
         getHashCodeMethod.Attributes.Should().NotHaveFlag(MethodAttributes.Abstract);
         getHashCodeMethod.DeclaringType.Should().Be(sutType);
      }

      [Fact]
      public void Object_Equals__Implementations_return_false_when_obj_argument_is_null()
      {
         var sut = this.CreateSubject();
         sut.Equals(null).Should().BeFalse();
      }


      [Fact]
      public void Object_Equals__Implementations_return_false_when_other_differs_in_implementation_type()
      {
         var other = new FakeIdentity();
         var sut = this.CreateSubject();
         sut.Equals((object)other).Should().BeFalse();
      }

      [Fact]
      public void Object_Equals__Implementations_return_true_when_passed_self_same_instance()
      {
         var sut = this.CreateSubject();

         sut.Equals((object)sut).Should().Be(true);
      }

      [Fact]
      public void IIdentity_Equals__Implementations_return_false_when_other_is_null()
      {
         var sut = this.CreateSubject();
         sut.Equals((IIdentity)null).Should().BeFalse();
      }

      [Fact]
      public void IIdentity_Equals__Implementations_return_false_when_other_differs_in_implementation_type()
      {
         var other = new FakeIdentity();
         var sut = this.CreateSubject();
         sut.Equals(other).Should().BeFalse();
      }

      [Fact]
      public void IIdentity_Equals__Implementations_return_true_when_passed_self_same_instance()
      {
         var sut = this.CreateSubject();

         sut.Equals(sut).Should().Be(true);
      }
      #endregion

      #region infrastructure
      protected abstract TIdentity CreateSubject();

      private class FakeIdentity : IIdentity
      {
         public bool Equals(IIdentity other)
         {
            throw new NotImplementedException();
         }
      }
      #endregion
   }
}