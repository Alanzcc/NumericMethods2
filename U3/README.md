# Métodos de Householder e QR em C#

Este projeto implementa dois algoritmos fundamentais em álgebra linear numérica:

1. **Redução de Householder** – utilizada para transformar uma matriz simétrica em uma matriz tridiagonal.
2. **Método QR** – utilizado para calcular autovalores e autovetores de matrizes.

As implementações usam a biblioteca [MathNet.Numerics](https://numerics.mathdotnet.com/).

---

## 1. Redução de Householder

A **transformação de Householder** é usada para reduzir uma matriz simétrica \( A \) em uma matriz tridiagonal \( T \), preservando seus autovalores.

A matriz de Householder \( H \) tem a forma:

\[
H = I - 2 \frac{vv^T}{v^T v},
\]

onde \( v \) é um vetor apropriado escolhido em cada passo da iteração.

### **Funções principais**
- `HouseholderMatrix(Matrix<double> A, int i)`  
  Gera uma matriz de Householder para a coluna \( i \) da matriz \( A \).

- `HouseholderMethod(Matrix<double> A)`  
  Aplica sucessivas reflexões de Householder para reduzir \( A \) a uma forma tridiagonal.  
  Retorna:
  - `Tri`: a matriz tridiagonal.
  - `H`: a matriz acumulada de transformações de Householder.

---

## 2. Decomposição e Método QR

A **decomposição QR** é uma fatoração de uma matriz \( A \) em um produto:

\[
A = Q R,
\]

onde:
- \( Q \) é uma matriz ortogonal.
- \( R \) é uma matriz triangular superior.

Combinando essa decomposição de forma iterativa, o **método QR** é usado para calcular autovalores e autovetores.

### **Funções principais**
- `QRDecomp(Matrix<double> A)`  
  Realiza a decomposição QR de uma matriz \( A \) utilizando rotações de Givens.  
  Retorna uma tupla `(Q, R)`.

- `QRMethod(Matrix<double> T, Matrix<double> H, double epsilon)`  
  Aplica o método QR iterativo a uma matriz tridiagonal \( T \), até que os elementos abaixo da diagonal sejam suficientemente pequenos (menores que `epsilon`).  
  Retorna:
  - `Lambda`: matriz diagonal aproximada com os autovalores.
  - `X`: matriz de autovetores.

---

## **Fluxo Completo**
1. A matriz \( A \) inicial é primeiro reduzida a forma tridiagonal \( T \) via `HouseholderMethod`.
2. O método `QRMethod` é aplicado sobre \( T \) para obter os autovalores (na diagonal de `Lambda`) e autovetores (em `X`).

---
