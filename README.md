# EmarsysDueDateCalculator
Coding homework @ Emarsys

## About testing
- I have only written tests in Green Fox.
- I did my best. I even tried TDD.

## About the code
- I've used Builder pattern to avoid creating incomplite objects.
- Also included some exception to handle wrong inputs.

## About TODO's
I left some comment to remind me of 
- implementing a DI container,
- decouple BugFixBuilder from DefaultWorkTimeValidator,
- handle the lifetime of objects.

## What I would refactor
- I checked my code again an I found some opportunity to refactor...
- DefaultWorkTimeValidator.IsOutOfWorkingHours method can be divided into smaller parts like IsAfterWorkingHours / IsBeforeWorkingHours / IsWeekend
- I.e. IsAfterWorkingHours already written in BugFixDueDateCalculator..
- It would improve readability and reusability.
- All const must be placed in DefaultWorkTimeValidator.
- WorkEndsAt could be calculated from WorkStartsAt and WorkingHours.

## +1
Something went wrong on the company website:
https://www.emarsys.com/en/technology-partners/
