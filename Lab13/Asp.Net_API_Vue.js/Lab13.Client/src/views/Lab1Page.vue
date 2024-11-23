<script>
export default {
  name: 'Lab1Page',
  data() {
    return {
      nValue: '',
      aValue: '',
      bValue: '',
      result: null,
      errorMessage: null,
      isLoading: false,
    }
  },

  created() {
    const token = localStorage.getItem('token')
    if (!token) {
      window.location.href = '/login'
    }
  },

  methods: {
    async submitForm(event) {
      event.preventDefault()
      this.isLoading = true
      this.errorMessage = null

      const inputData = `${this.nValue} ${this.aValue} ${this.bValue}`
      const serverUrl = import.meta.env.VITE_SERVER_URL

      const response = await fetch(`${serverUrl}/lab1`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(inputData),
      })

      if (response.ok) {
        const result = await response.text()
        this.result = result
        this.isLoading = false
      } else {
        const errorText = await response.text()
        this.errorMessage = `Помилка: ${errorText}`
        this.isLoading = false
      }
    },
  },
}
</script>

<template>
  <div>
    <h1 class="text-center">Lab1</h1>
    <div class="flex-container">
      <section class="task-section">
        <h3>Завдання</h3>
        <b>Варіант 17</b>
        <p>
          У вас є N збудованих в ряд коробок, червоних і синіх куль. Усі червоні кулі (аналогічно та
          сині) ідентичні. Ви можете класти кулі у коробки. Дозволяється розміщувати в коробках кулі
          як одного, так і двох видів одночасно. Також дозволяється залишати деякі з коробок
          порожніми. Не обов'язково класти всі кулі у коробки.
        </p>

        <h4>
          Потрібно написати програму, яка визначає кількість різних способів, якими можна заповнити
          коробки кулями.
        </h4>

        <h5>Вхідні дані</h5>
        <p>
          Вхідний файл <strong>INPUT.TXT</strong> містить цілі числа N, A, B. (1 ≤ N ≤ 20, 0 ≤ A, B
          ≤ 20)
        </p>

        <h5>Вихідні дані</h5>
        <p>У вихідний файл <strong>OUTPUT.TXT</strong> виведіть відповідь на завдання.</p>

        <h5>Приклади</h5>
        <table>
          <thead>
            <tr>
              <th>№</th>
              <th>INPUT.TXT</th>
              <th>OUTPUT.TXT</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>1 1 1</td>
              <td>4</td>
            </tr>
            <tr>
              <td>2</td>
              <td>2 1 1</td>
              <td>9</td>
            </tr>
          </tbody>
        </table>
      </section>
      <section class="solution-section">
        <div>
          <h3>Введіть ваші дані:</h3>
          <form @submit="submitForm">
            <label for="nValue">N - Кількість коробок:</label>
            <input
              type="number"
              v-model="nValue"
              id="nValue"
              name="nValue"
              min="1"
              max="20"
              required
            />

            <label for="aValue">A - Кількість червоних куль:</label>
            <input
              type="number"
              v-model="aValue"
              id="aValue"
              name="aValue"
              min="0"
              max="20"
              required
            />

            <label for="bValue">B - Кількість зелених куль:</label>
            <input
              type="number"
              v-model="bValue"
              id="bValue"
              name="bValue"
              min="0"
              max="20"
              required
            />

            <button type="submit" :disabled="isLoading">Відправити</button>
          </form>
        </div>
        <div class="result">
          <h3>Результат:</h3>
          <p v-if="isLoading">Завантаження...</p>
          <p v-if="result">{{ result }}</p>
        </div>
        <div v-if="errorMessage">
          <p class="error">{{ errorMessage }}</p>
        </div>
      </section>
    </div>
  </div>
</template>

<style scoped>
.error {
  color: red;
}
</style>
