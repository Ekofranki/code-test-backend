# Top 5 areas of concern
1. I think, that strategy pattern may be applied instead of 'ifology' pattern :) Currently we have low cohesion, a lot of branches in SubmitApplicationFor method - it makes difficult to write  rational unit tests (detected another vulnerability radar - violated SRP). Using MediatR here would be nice.
2. Return statements currently are not readable and are duplicated, moving these logic to sepearate dispatchers should avoid problem.
3. Encapsulation need to be improved.
4. There is almost no exception handling.
5. High complexity level in SubmitApplicationFor method - this could be fixed also by seperating business logic and delegate execution of it to dedicated dispatchers.
