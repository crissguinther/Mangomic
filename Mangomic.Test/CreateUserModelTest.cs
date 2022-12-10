using Mangomic.Model;

namespace Mangomic.Test {
    [TestClass]
    public class CreateUserModelTest {
        [TestMethod]
        public void Assert_Password_is_Hashed() {
            string pass = "password";
            CreateUserModel user = new CreateUserModel("test_username", "test@test.com", pass);

            Assert.AreNotEqual(user.PasswordHash, pass);
        }
    }
}