<script>
export default {
  data() {
    return {
      form: {
        username: '',
        fullName: '',
        password: '',
        passwordConfirm: '',
        phone: '',
        email: '',
      },
      errorMessage: '',
      loading: false,
    }
  },
  methods: {
    async handleRegistration() {
      if (this.form.password !== this.form.passwordConfirm) {
        this.errorMessage = 'Паролі не збігаються!'
        return
      }

      this.loading = true
      this.errorMessage = ''
      const serverUrl = import.meta.env.VITE_SERVER_URL

      try {
        const response = await fetch(`${serverUrl}/register`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            username: this.form.username,
            fullName: this.form.fullName,
            password: this.form.password,
            phone: this.form.phone,
            email: this.form.email,
          }),
        })

        if (!response.ok) {
          const data = await response.json()
          this.errorMessage = data.message || 'Помилка реєстрації.'
          this.loading = false
          return
        }

        const loginResponse = await fetch(`${serverUrl}/account/login`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            username: this.form.username,
            password: this.form.password,
          }),
        })

        if (!loginResponse.ok) {
          const loginData = await loginResponse.json()
          this.errorMessage = loginData.message || 'Помилка входу.'
        } else {
          this.$router.push({ name: 'profile' })
        }
      } catch (error) {
        this.errorMessage = 'Помилка на сервері.'
      } finally {
        this.loading = false
      }
    },
  },
}
</script>

<template>
  <div>
    <h1 class="text-center">Registration</h1>
    <section>
      <form @submit.prevent="handleRegistration" id="registrationForm">
        <label for="username">Ім'я користувача:</label>
        <input
          type="text"
          v-model="form.username"
          id="username"
          name="username"
          maxlength="50"
          required
        />

        <label for="fullName">ФІО:</label>
        <input
          type="text"
          v-model="form.fullName"
          id="fullName"
          name="fullName"
          maxlength="500"
          required
        />

        <label for="password">Пароль (8-16 символів, 1 цифра, 1 знак, 1 велика літера):</label>
        <input
          type="password"
          v-model="form.password"
          id="password"
          name="password"
          minlength="8"
          maxlength="16"
          required
        />

        <label for="passwordConfirm">Підтвердження паролю:</label>
        <input
          type="password"
          v-model="form.passwordConfirm"
          id="passwordConfirm"
          name="passwordConfirm"
          minlength="8"
          maxlength="16"
          required
        />

        <label for="phone">Телефон:</label>
        <input
          type="tel"
          v-model="form.phone"
          id="phone"
          name="phone"
          pattern="^\+380\d{9}$"
          required
          placeholder="+380XXXXXXXXX"
        />

        <label for="email">Електронна адреса:</label>
        <input type="email" v-model="form.email" id="email" name="email" required />

        <button type="submit" :disabled="loading">Зареєструватися</button>
      </form>

      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </section>
  </div>
</template>
