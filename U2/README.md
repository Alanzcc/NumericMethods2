# Numerical Integration Library

Este projeto contém implementações de **métodos numéricos de integração** usando **fórmulas de quadratura de Gauss**, **fórmulas de Newton-Cotes (fechadas e abertas)**, e **transformações exponenciais** para intervalos infinitos.  

---

## ?? **Conteúdo**
- **Exponential.cs**  
  - Implementa transformações exponenciais para lidar com integrais impróprias (intervalos infinitos).
  - Usa quadratura de Gauss-Hermite para avaliar integrais da forma:
    \[
    \int_a^b f(x) \, dx
    \]
    com transformações:
    - `Simple()` – Transformação usando `tanh(s)`  
    - `Double()` – Transformação dupla com `sinh(s)`

- **GaussQuadrature/**  
  Contém classes para diferentes polinômios ortogonais e suas respectivas fórmulas de quadratura:
  - `Legendre` – Quadratura de Gauss-Legendre.
  - `Hermite` – Quadratura de Gauss-Hermite (útil para pesos \( e^{-x^2} \)).
  - `Laguerre` – Quadratura de Gauss-Laguerre (útil para integrais em \([0, \infty)\)).
  - `Chebyshev` – Quadratura de Gauss-Chebyshev.

- **NewtonCotes/**  
  Contém as implementações das fórmulas fechadas e abertas de Newton-Cotes:
  - `Closed` – Trapezoidal, Simpson, 3/8 de Simpson, e Boole.
  - `Open` – Fórmulas abertas de 1º a 4º grau, com composição adaptativa usando **recursão e tolerância de erro**.

---
