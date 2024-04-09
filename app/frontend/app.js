import axios from "axios";

const BASE_URL = "http://localhost:5126/api/v1/exrates";

const button = document.getElementById("button");
const status = document.getElementById("status");
const result = document.getElementById("result");

button.addEventListener("click", async _ => {
    button.disabled = !button.disabled;
    status.innerText = "IN PROGRESS";
    result.innerText = "";
    try {
        const res = await axios.get(BASE_URL, { headers: { "Accept": "application/json" } });
        status.innerText = "SUCCESS";
        result.innerText = res.data
            .map(item => `${item.sourceCurrency} / ${item.targetCurrency} = ${item.value}`)
            .join("\n");
    }
    catch (ex) {
        status.innerText = "FAILURE";
        result.innerText = `${ex}`
    }
    button.disabled = !button.disabled;
});
