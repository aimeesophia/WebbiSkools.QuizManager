var common = (function () {
    // Public functions
    function addEventBindings() {
        $(document).on("click", ".add-question-button", function () {
            addQuestion();
        });

        $(document).on("click", ".add-answer-button", function () {
            addAnswer(this);
        });

        $(document).on("click", ".delete-question-button", function () {
            deleteQuestion(this);
        });

        $(document).on("click", ".delete-answer-button", function () {
            deleteAnswer(this);
        });

        $("#create-form-submit-button").click(function (event) {
            event.preventDefault();

            updateQuestionsAndAnswersAttributes();

            $("#create-form").submit();
        });

        $("#edit-form-submit-button").click(function (event) {
            event.preventDefault();

            updateQuestionsAndAnswersAttributes();

            $("#edit-form").submit();
        });
    }

    // Private functions
    function addQuestion() {
        $("#all-questions-and-answers").append(createQuestionAndAnswersGroup());
    }

    function createQuestionAndAnswersGroup() {
        var questionFormGroupHtml = '<div class="question-and-answers-group mb-3">' +
            '<div class="question">' +
            '<div class="form-group">' +
            '<input asp-for="TOBECHANGED" class="form-control question-text-input" name="TOBECHANGED" placeholder="Question" />' +
            '<span asp-validation-for="TOBECHANGED" class="text-danger"></span>' +
            '</div>' +
            '</div>' +
            '<div class="answers">' +
            '<div class="form-group answer">' +
            '<div class="input-group">' +
            '<input asp-for="TOBECHANGED" class="form-control answer-text-input" name="TOBECHANGED" placeholder="Answer" />' +
            '<button type="button" class="btn btn-danger delete-answer-button">Delete Answer</button>' +
            '</div>' +
            '<span asp-validation-for="TOBECHANGED"></span>' +
            '</div>' +
            '<div class="form-group answer">' +
            '<div class="input-group">' +
            '<input asp-for="TOBECHANGED" class="form-control answer-text-input" name="TOBECHANGED" placeholder="Answer" />' +
            '<button type="button" class="btn btn-danger delete-answer-button">Delete Answer</button>' +
            '</div>' +
            '<span asp-validation-for="TOBECHANGED"></span>' +
            '</div>' +
            '<div class="form-group answer">' +
            '<div class="input-group">' +
            '<input asp-for="TOBECHANGED" class="form-control answer-text-input" name="TOBECHANGED" placeholder="Answer" />' +
            '<button type="button" class="btn btn-danger delete-answer-button">Delete Answer</button>' +
            '</div>' +
            '<span asp-validation-for="TOBECHANGED"></span>' +
            '</div>' +
            '</div>' +
            '<button type="button" class="btn btn-success add-question-button">Add Question</button>' +
            '<button type="button" class="btn btn-success add-answer-button">Add Answer</button>' +
            '<button type="button" class="btn btn-danger delete-question-button">Delete Question</button>' +
            '</div>';

        return questionFormGroupHtml;
    }

    function addAnswer(addAnswerButton) {
        $(addAnswerButton).prevAll(".answers").append(createAnswerFormGroup());
    }

    function createAnswerFormGroup() {
        var answerFormGroupHtml = '<div class="form-group answer">' +
            '<div class="input-group">' +
            '<input asp-for="TOBECHANGED" class="form-control answer-text-input" name="TOBECHANGED" placeholder="Answer" />' +
            '<button type="button" class="btn btn-danger delete-answer-button">Delete Answer</button>' +
            '</div>' +
            '<span asp-validation-for="TOBECHANGED"></span>' +
            '</div>';

        return answerFormGroupHtml;
    }

    function deleteQuestion(deleteQuestionButton) {
        $(deleteQuestionButton).parents(".question-and-answers-group").remove();
    }

    function deleteAnswer(deleteAnswerButton) {
        $(deleteAnswerButton).parents(".answer").remove();
    }

    function updateQuestionsAndAnswersAttributes() {
        var questionCount = 0;
        var questionAndAnswersGroups = $(".question-and-answers-group");

        questionAndAnswersGroups.each(function () {
            var questionAnswerFormGroup = $(this);
            var answerCount = 0;

            questionAnswerFormGroup.find(".question-text-input").attr("name", "Questions[" + questionCount + "].Text");

            var answers = questionAnswerFormGroup.find(".answer");

            answers.each(function () {
                var answer = $(this);

                answer.find(".answer-text-input").attr("name", "Questions[" + questionCount + "].Answers[" + answerCount + "].Text");

                answerCount++;
            });

            questionCount++;
        });
    }

    // Exposed functions
    return {
        addEventBindings: addEventBindings
    }
})();

$(document).ready(function () {
    common.addEventBindings();
});