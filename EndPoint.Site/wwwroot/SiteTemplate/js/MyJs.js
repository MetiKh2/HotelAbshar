


function validateEmail(emailaddress) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(emailaddress)) {
        alert("لطفا ایمیل خود را به درستی وارد کنید");
        event.preventDefault();
    }
}


//چک کردن داده ها برای ثبت نام کاربران
const formRegister = document.querySelector('#formRegister');
formRegister.addEventListener('submit', event => {
    let fullName = document.getElementById('fullName').value;
    let userName = document.getElementById('userName').value;
    let email = document.getElementById('email').value;
    let password = document.getElementById('password').value;
    let rePassword = document.getElementById('rePassword').value;

    if (userName == "" || fullName == "" || email == "" || password == "" || rePassword == "") {
        alert("لطفا تمامی موارد را وارد کنید");
        event.preventDefault();
    }
    validateEmail(email);

    // actual logic, e.g. validate the form
    console.log('Form submission cancelled.');
});
//فراموشی رمز عبور
const formFotgotPassword = document.querySelector('#formFotgotPassword');
formFotgotPassword.addEventListener('submit', event => {
  
    let emailForgotPassword = document.getElementById('emailForgotPassword').value;


    if (emailForgotPassword == "") {
        alert("لطفا تمامی موارد را وارد کنید");
        event.preventDefault();
    }
    validateEmail(emailForgotPassword);

    // actual logic, e.g. validate the form
    console.log('Form submission cancelled.');
});