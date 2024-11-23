<script>
    export default {
        name: "SignupPage",
    };
</script>

<template>
    <div>
        <h1 class="text-center">Registration</h1>
        <section>
            <form method="post" id="registrationForm" action="/account/register">
                <label for="username">Ім'я користувача:</label>
                <input type="text" id="username" name="username" maxlength="50" required>

                <label for="fullName">ФІО:</label>
                <input type="text" id="fullName" name="fullName" maxlength="500" required>

                <label for="password">Пароль (8-16 символів, 1 цифра, 1 знак, 1 велика літера):</label>
                <input type="password" id="password" name="password" minlength="8" maxlength="16" required>

                <label for="passwordConfirm">Підтвердження паролю:</label>
                <input type="password" id="passwordConfirm" name="passwordConfirm" minlength="8" maxlength="16" required>

                <label for="phone">Телефон:</label>
                <input type="tel" id="phone" name="phone" pattern="^\+380\d{9}$" required placeholder="+380XXXXXXXXX">

                <label for="email">Електронна адреса:</label>
                <input type="email" id="email" name="email" required>

                <button type="submit">Зареєструватися</button>
            </form>
        </section>
    </div>
</template>
