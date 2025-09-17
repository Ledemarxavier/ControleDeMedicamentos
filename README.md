#  Controle de Medicamentos

O **Controle de Medicamentos** é um sistema desenvolvido para gerenciar fornecedores, pacientes, medicamentos, funcionários e prescrições médicas, garantindo o controle do estoque de forma segura e eficiente.  

---

## Especificação do Projeto - Requisitos

### 1. Módulo de Fornecedores
**Requisitos Funcionais**
- Registrar novos fornecedores  
- Visualizar todos os fornecedores cadastrados  
- Editar fornecedores existentes  
- Excluir fornecedores cadastrados  

**Regras de Negócio**
- Campos obrigatórios: Nome (3-100), Telefone (formatos válidos), CNPJ (14 dígitos)  
- Não permitir cadastro com CNPJ duplicado  

---

### 2. Módulo de Pacientes
**Requisitos Funcionais**
- Registrar novos pacientes  
- Visualizar todos os pacientes cadastrados  
- Editar pacientes existentes  
- Excluir pacientes cadastrados  

**Regras de Negócio**
- Campos obrigatórios: Nome (3-100), Telefone (formatos válidos), Cartão do SUS (15 dígitos), CPF (11 dígitos)  
- Não permitir cadastro com Cartão do SUS duplicado  

---

### 3. Módulo de Medicamentos
**Requisitos Funcionais**
- Registrar novos medicamentos  
- Visualizar todos os medicamentos cadastrados  
- Editar medicamentos existentes  
- Excluir medicamentos cadastrados  

**Regras de Negócio**
- Campos obrigatórios: Nome (3-100), Descrição (5-255), Quantidade em estoque (número positivo), Fornecedor  
- Destacar medicamentos com menos de 20 unidades como **"em falta"**  
- Atualizar quantidade caso o medicamento já esteja cadastrado  

---

### 4. Módulo de Funcionários
**Requisitos Funcionais**
- Registrar novos funcionários  
- Visualizar todos os funcionários cadastrados  
- Editar funcionários existentes  
- Excluir funcionários cadastrados  

**Regras de Negócio**
- Campos obrigatórios: Nome (3-100), Telefone (formatos válidos), CPF (11 dígitos)  
- Não permitir cadastro com CPF duplicado  

---

### 5. Módulo de Prescrições Médicas
**Requisitos Funcionais**
- Cadastrar novas prescrições médicas  
- Gerar relatórios de prescrições  

**Regras de Negócio**
- Campos obrigatórios: CRM do médico (6 dígitos), Paciente, Data (válida), Lista de medicamentos (com dosagem e período)  
- Exigir prescrição válida para requisições de saída  

---

### 6. Módulo de Estoque
#### Requisições de Entrada
**Requisitos Funcionais**
- Registrar novas requisições de entrada  
- Visualizar todas as requisições de entrada  

**Regras de Negócio**
- Campos obrigatórios: Data (válida), Medicamento, Funcionário, Quantidade (número positivo)  
- Atualizar estoque ao registrar entrada  

#### Requisições de Saída
**Requisitos Funcionais**
- Registrar novas requisições de saída  
- Visualizar todas as requisições de saída  

**Regras de Negócio**
- Campos obrigatórios: Data (válida), Paciente, Prescrição Médica, Medicamentos requisitados  
- Não permitir requisição que exceda estoque disponível  
- Subtrair quantidade do estoque ao registrar saída  

---

## 🛠 Tecnologias Utilizadas
[![My Skills](https://skillicons.dev/icons?i=visualstudio,dotnet,cs,git,github,bootstrap,html,css,javascript)](https://skillicons.dev) 

---

