# Métodos Numéricos para Equações Diferenciais Ordinárias (EDO) em C#

Este projeto contém implementações em C# dos principais métodos numéricos para solução de EDOs: **Euler Explícito**, **Euler Implícito**, **Runge-Kutta** (ordens 2, 3 e 4) e métodos **Predictor-Corrector** (Adams-Bashforth de 2ª, 3ª e 4ª ordem). Todas as implementações utilizam a biblioteca [MathNet.Numerics](https://numerics.mathdotnet.com/) para manipulação de vetores e matrizes.

---

## Estrutura do Código

### Euler

- **ExplicitEuler**: Implementa o método de Euler explícito, que calcula o próximo estado usando o valor da derivada no ponto atual.
- **ImplicitEuler**: Implementa o método de Euler implícito, usando Euler explícito como preditor e corrigindo o próximo estado com base na derivada no ponto previsto.

### Runge-Kutta

- **RungeKuttaSecondOrder**: Método Runge-Kutta de 2ª ordem, que utiliza Euler implícito para previsão.
- **RungeKuttaThirdOrder**: Método Runge-Kutta de 3ª ordem, com duas etapas intermediárias e predição por Euler explícito.
- **RungeKuttaFourthOrder**: Método clássico de Runge-Kutta de 4ª ordem (RK4), usando quatro avaliações de derivada para obter alta precisão.

### Predictor-Corrector (Adams-Bashforth)

- **AdamsBashforth2**: Método predictor-corrector de 2ª ordem, utilizando duas etapas para previsão e correção iterativa.
- **AdamsBashforth3**: Método predictor-corrector de 3ª ordem, com três etapas e correção iterativa.
- **AdamsBashforth4**: Método predictor-corrector de 4ª ordem, com quatro etapas e correção iterativa até que o erro relativo seja menor que a tolerância definida.

---

## Como usar

- Defina a função derivada `DerivativeFunc`, que recebe o estado atual, índice e tempo, e retorna a derivada.
- Inicialize o vetor de condições iniciais usando `Vector<double>`.
- Escolha o método desejado e chame o método `Execute`, passando a função derivada, estado inicial, tempo inicial, passo `h` e, no caso do Predictor-Corrector, a tolerância de erro.
- O método retornará o próximo tempo e estado calculado.

---
