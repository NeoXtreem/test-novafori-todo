describe('The To-Dos main page', () => {
  it('adds a pending to-do when one is entered', () => {
    // Arrange
    cy.visit('https://localhost:7019');
    cy.get('table').first().find('tr').its('length').as('initialCount');
    const description = 'Test to-do';

    // Act
    cy.get('input').type(description);
    cy.contains('Add').click();

    // Assert
    cy.get('@initialCount').then(initial => {
      const tables = cy.get('table').first().find('tr');
      tables.should('have.length', initial + 1);
      tables.last().contains(description);
    });
  });
})