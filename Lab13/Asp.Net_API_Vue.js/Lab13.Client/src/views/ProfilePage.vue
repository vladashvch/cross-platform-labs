<script>
export default {
  name: 'ProfilePage',

  data() {
    return {
      token: localStorage.getItem('token'),
      userData: null,
      errorMessage: null,
      isLoading: true,
    }
  },

  mounted() {
    if (!this.token) {
      this.redirectToLogin()
      return
    }
    this.fetchUserProfile()
  },

  methods: {
    async fetchUserProfile() {
      const serverUrl = import.meta.env.VITE_SERVER_URL
      
      try {
        const response = await fetch(`${serverUrl}/get-profile`, {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        })

        if (!response.ok) {
          const errorData = await response.json()
          throw new Error(errorData.message || 'Failed to fetch profile')
        }

        this.userData = await response.json()
        this.isLoading = false
      } catch (error) {
        this.errorMessage = error.message
        this.isLoading = false

        if (error.message.includes('Unauthorized')) {
          localStorage.removeItem('token')
          setTimeout(this.redirectToLogin, 2000)
        }
      }
    },

    redirectToLogin() {
      window.location.href = '/login'
    },
  },
}
</script>

<template>
  <div>
    <h1>Ваш профіль</h1>

    <div v-if="isLoading">Завантаження...</div>

    <div v-if="!isLoading && userData">
      <p>
        <strong>Ім'я користувача:</strong> <span>{{ userData.nickname || 'Не вказано' }}</span>
      </p>
      <p>
        <strong>ФІО:</strong> <span>{{ userData.name || 'Не вказано' }}</span>
      </p>
      <p>
        <strong>Телефон:</strong> <span>{{ userData.phone || 'Не вказано' }}</span>
      </p>
      <p>
        <strong>Електронна адреса:</strong> <span>{{ userData.email || 'Не вказано' }}</span>
      </p>
    </div>

    <div v-if="errorMessage" class="alert alert-danger">
      {{ errorMessage }}
    </div>
  </div>
</template>
