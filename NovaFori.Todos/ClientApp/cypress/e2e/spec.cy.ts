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
      const rows = cy.get('table').first().find('tr');
      rows.should('have.length', initial + 1);
      rows.last().contains(description);
    });
  });

  it('moves a pending to-do to completed when marked complete', () => {
    testTodoToggle('✅', 1)
  });

  it('moves a pending to-do to completed when marked complete', () => {
    testTodoToggle('❌', -1)
  });

  function testTodoToggle(button, delta) {
    // Arrange
    cy.visit('https://localhost:7019');
    cy.get('table').first().find('tr').its('length').as('initialCount1');
    cy.get('table').last().find('tr').its('length').as('initialCount2');

    // Act
    cy.contains(button).click();

    // Assert
    cy.get('@initialCount1').then(initial => {
      const rows = cy.get('table').first().find('tr');
      rows.should('have.length', initial - 1 * delta);
    });

    cy.get('@initialCount2').then(initial => {
      const rows = cy.get('table').last().find('tr');
      rows.should('have.length', initial + 1 * delta);
    });
  };
})