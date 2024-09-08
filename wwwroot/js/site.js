const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('deleteToast')
if (toastTrigger) {
    toastTrigger.addEventListener('click', () => {
        const toast = new bootstrap.Toast(toastLiveExample)

        toast.show()
    })
}


const stepper = new mdb.Stepper(document.getElementById('stepper-form-example'));

document.getElementById('form-example-next-step').addEventListener('click', () => {
  stepper.nextStep();
});

document.getElementById('form-example-prev-step').addEventListener('click', () => {
  stepper.previousStep();
});