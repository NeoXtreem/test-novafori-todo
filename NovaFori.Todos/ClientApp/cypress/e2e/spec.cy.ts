describe('The To-Dos main page', () => {
  it('displays two empty lists', () => {
    // Arrange
    cy.visit('https://localhost:7019');

    // Assert
    cy.get('table').each(($el) => cy.wrap($el).find('tr').should('have.length', 1));
  })
})