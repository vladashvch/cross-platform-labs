<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const token = ref(null)
const isAuthorized = ref(false)

function logout() {
  localStorage.removeItem('token')
  isAuthorized.value = false
  router.push('/')
}

onMounted(() => {
  token.value = localStorage.getItem('token')
  isAuthorized.value = !!token.value
})
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div
        class="navbar-collapse collapse d-sm-inline-flex justify-content-between align-items-center"
      >
        <ul class="navbar-nav flex-grow-1 d-flex align-items-center">
          <li class="nav-item">
            <RouterLink to="/" class="nav-link text-dark">Home</RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/lab1" class="nav-link text-dark">Lab1</RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/lab2" class="nav-link text-dark">Lab2</RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/lab3" class="nav-link text-dark">Lab3</RouterLink>
          </li>
        </ul>

        <ul class="navbar-nav d-flex align-items-center">
          <li class="nav-item">
            <RouterLink to="/signup" class="nav-link text-dark unauthorize-links"
              >Signup</RouterLink
            >
          </li>
          <li class="nav-item">
            <RouterLink to="/login" class="nav-link text-dark unauthorize-links">Login</RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/profile" class="nav-link text-dark unauthorize-links">
              Profile
            </RouterLink>
          </li>
          <li class="nav-item">
            <a class="nav-link text-dark unauthorize-links" @click="logout()">Logout</a>
          </li>
        </ul>
      </div>
    </nav>
  </header>

  <main role="main" class="pb-3">
    <RouterView />
  </main>

  <footer class="border-top footer text-muted">
    <div class="container">&copy; 2024 - Lab5</div>
  </footer>
</template>

<style>

</style>
