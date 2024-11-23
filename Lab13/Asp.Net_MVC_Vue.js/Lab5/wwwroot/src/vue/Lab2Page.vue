<script>
    export default {
        name: "Lab2Page",
        data() {
            return {
                nValue: '',
                result: null,
                errorMessage: null,
                isLoading: false
            };
        },

        created() {
            const token = localStorage.getItem('token');
            if (!token) {
                window.location.href = '/login';
            }
        },

        methods: {
            async submitForm(event) {
                event.preventDefault();
                this.isLoading = true;
                this.errorMessage = null;

                const n = parseInt(this.nValue);
                const inputData = `${n}`;
                console.log('Відправляємо значення:', inputData);

                const response = await fetch('/get-labs/lab2', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(inputData)
                });

                if (response.ok) {
                    const result = await response.text();
                    this.result = result;
                    this.isLoading = false;
                } else {
                    const errorText = await response.text();
                    this.errorMessage = `Помилка: ${errorText}`;
                    this.isLoading = false;
                }
            }
        }
    };
</script>

<template>
    <div>
        <h1 class="text-center">Lab2</h1>
        <div class="flex-container">
            <section class="task-section">
                <h3>Завдання</h3>
                <b>Варіант 17</b>
                <p>Будемо називати натуральне число трипростим, якщо в ньому будь-які 3 цифри, що поспіль йдуть, утворюють тризначне просте число. Потрібно знайти кількість N-значних трипростих чисел.</p>

                <h4>Вхідні дані</h4>
                <p>Вхідний файл <strong>INPUT.TXT</strong> містить натуральне число N (3 ≤ N ≤ 10000).</p>

                <h4>Вихідні дані</h4>
                <p>У вихідний файл <strong>OUTPUT.TXT</strong> повинен містити кількість N-значних трипростих чисел, яку слід вивести за модулем <code>10<sup>9</sup> + 9</code>.</p>

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
                            <td>4</td>
                            <td>204</td>
                        </tr>
                    </tbody>
                </table>
            </section>
            <section class="solution-section">
                <div>
                    <h3>Введіть ваші дані:</h3>
                    <form @submit="submitForm">
                        <label for="nValue">N - Натуральне число від 3 до 10000:</label>
                        <input type="number" v-model="nValue" id="nValue" name="nValue" min="3" max="10000" required>

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
