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
                <label for="username">��'� �����������:</label>
                <input type="text" id="username" name="username" maxlength="50" required>

                <label for="fullName">Բ�:</label>
                <input type="text" id="fullName" name="fullName" maxlength="500" required>

                <label for="password">������ (8-16 �������, 1 �����, 1 ����, 1 ������ �����):</label>
                <input type="password" id="password" name="password" minlength="8" maxlength="16" required>

                <label for="passwordConfirm">ϳ����������� ������:</label>
                <input type="password" id="passwordConfirm" name="passwordConfirm" minlength="8" maxlength="16" required>

                <label for="phone">�������:</label>
                <input type="tel" id="phone" name="phone" pattern="^\+380\d{9}$" required placeholder="+380XXXXXXXXX">

                <label for="email">���������� ������:</label>
                <input type="email" id="email" name="email" required>

                <button type="submit">��������������</button>
            </form>
        </section>
    </div>
</template>
