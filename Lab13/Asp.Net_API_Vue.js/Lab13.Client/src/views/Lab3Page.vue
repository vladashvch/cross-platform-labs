<script>
export default {
  name: 'Lab3Page',
  data() {
    return {
      procedureInput: '',
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
      const inputData = this.procedureInput
      const serverUrl = import.meta.env.VITE_SERVER_URL

      const response = await fetch(`${serverUrl}/lab3`, {
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
    <h1 class="text-center">Lab3</h1>
    <div class="flex-container">
      <section class="task-section">
        <h3>Завдання</h3>
        <b>Варіант 17</b>
        <p>
          Одним із важливих понять, що використовуються в теорії алгоритмів, є рекурсія. Неформально
          її можна визначити як використання в описі об'єкта себе. Якщо йдеться про процедуру, то в
          процесі виконання ця процедура безпосередньо чи опосередковано (через інші процедури)
          викликає сама себе.
        </p>
        <p>
          Рекурсія є дуже «потужним» методом побудови алгоритмів, але приховує деякі небезпеки.
          Наприклад, неакуратно написана рекурсивна процедура може увійти в нескінченну рекурсію,
          тобто ніколи не закінчити своє виконання (насправді виконання закінчиться з переповненням
          стека).
        </p>
        <p>
          Оскільки рекурсія може бути непрямою (процедура викликає сама себе через інші процедури),
          то завдання визначення того факту, чи ця процедура є рекурсивною, досить складна.
          Спробуємо розв'язати просте завдання.
        </p>
        <p>
          Розглянемо програму, що складається з n процедур P<sub>1</sub>, P<sub>2</sub>, …,
          P<sub>n</sub>. Нехай кожної процедури відомі процедури, які може викликати. Процедура P
          називається потенційно рекурсивною, якщо існує така послідовність процедур Q<sub>0</sub>,
          Q<sub>1</sub>, …, Q<sub>k</sub>, що Q<sub>0</sub> = Q<sub>k</sub> = P і для i = 1 ... k
          процедура Q<sub>i-1</sub> може викликати процедуру Q<sub>i</sub>. У цьому випадку завдання
          полягатиме у визначенні для кожної із заданих процедур, чи є вона потенційно рекурсивною.
        </p>

        <h4>Вхідні дані</h4>
        <p>
          Перший рядок вхідного файлу <strong>INPUT.TXT</strong> містить ціле число n — кількість
          процедур у програмі (1 ≤ n ≤ 100). Далі йдуть n блоків, що описують процедури. Після
          кожного блоку слідує рядок, який містить 5 символів «*».
        </p>
        <p>
          Опис процедури починається з рядка, що містить його ідентифікатор, що складається лише з
          маленьких букв англійського алфавіту та цифр. Ідентифікатор не порожній, і його довжина
          вбирається у 100 символів. Далі йде рядок, що містить число k (k ≤ n) - кількість
          процедур, які можуть бути викликані процедурою, що описується. Наступні k рядків містять
          ідентифікатори цих процедур по одному ідентифікатору на рядку.
        </p>
        <p>
          Різні процедури мають різні ідентифікатори. При цьому жодна процедура не може викликати
          процедуру, яка не описана у вхідному файлі.
        </p>

        <h4>Вихідні дані</h4>
        <p>
          У вихідний файл <strong>OUTPUT.TXT</strong> для кожної процедури, що є у вхідних даних,
          необхідно вивести слово YES, якщо вона є потенційно рекурсивною, і слово NO – інакше, у
          тому порядку, як вони перераховані у вхідних даних.
        </p>

        <h4>Приклади</h4>
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
              <td>3</td>
              <td>YES</td>
            </tr>
            <tr>
              <td></td>
              <td>p1</td>
              <td>YES</td>
            </tr>
            <tr>
              <td></td>
              <td>2</td>
              <td>NO</td>
            </tr>
            <tr>
              <td></td>
              <td>p1</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>p2</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>*****</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>p2</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>1</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>p1</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>*****</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>p3</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>1</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>p1</td>
              <td></td>
            </tr>
            <tr>
              <td></td>
              <td>*****</td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </section>
      <section class="solution-section">
        <div>
          <h3>Введіть ваші дані:</h3>
          <form @submit="submitForm">
            <label for="procedureInput">Один ідентифікатор на рядок:</label>
            <textarea
              v-model="procedureInput"
              id="procedureInput"
              name="procedureInput"
              rows="20"
              cols="10"
              required
            >
            </textarea>
            <button type="submit">Відправити</button>
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
