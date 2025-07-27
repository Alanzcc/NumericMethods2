# Método de Diferenças Finitas para Equação Diferencial em C#

Este código implementa a solução de uma equação diferencial ordinária do tipo

\[
u''(x) + 7 u'(x) - u(x) = 2,
\]

com condições de contorno de Dirichlet

\[
u(0) = 10, u(2) = 1,
\]

utilizando o método de **Diferenças Finitas** com passo \( \Delta x = 0.1 \).

---

## Detalhes do Método

- As derivadas são aproximadas por diferenças centrais de segunda ordem:
  - Derivada primeira:
    \[
    u'(x) * {u(x+h) - u(x-h)} /{2h}
    \]
  - Derivada segunda:
    \[
    u''(x) * {u(x+h) - 2 u(x) + u(x-h)} / {h^2}
    \]

- O sistema linear resultante tem a forma \( A \mathbf{u} = \mathbf{b} \), onde
  - A matriz \( A \) é tridiagonal, construída com coeficientes derivados da discretização das derivadas e do termo \( -u(x) \).
  - O vetor \( b \) contém o termo constante da equação (no caso, valor 2 para cada ponto interno) e as condições de contorno são incorporadas no vetor \( b \).

---

## Estrutura do Código

- A função principal `Single` recebe:
  - `init_cond_x` e `init_cond_y`: ponto inicial e valor da condição de contorno em \( x = 0 \).
  - `end_cond_x` e `end_cond_y`: ponto final e valor da condição de contorno em \( x = 2 \).
  - `h`: passo de discretização (exemplo: 0.1).

- Calcula o número de pontos internos para a aproximação.
- Monta a matriz \( A \) e o vetor \( b \) conforme as fórmulas discretizadas, incorporando as condições de contorno.

- Resolve o sistema linear usando o método `Solve` da biblioteca [MathNet.Numerics](https://numerics.mathdotnet.com/).

---
