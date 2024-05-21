import { tableData, deleteSubtheme } from './themaListRest';
type SubTheme = {
    description: string;
    title: string;
    id: string;
};
async function fillSubthemesTable(): Promise<void> {
    try {
        const subThemas: SubTheme[] = await tableData();

        let output = document.querySelector("#SubthemaTable");
        let bodyData = ``;

        for (const subThema of subThemas) {
            bodyData += `<tr data-description="${subThema.description}">
                        <td>${subThema.title}</td>
                        <td><a class="bi bi-pencil-square" href=""></a></td>
                        <td><i class="bi bi-trash deleteIcon" data-id="${subThema.id}"></i></td>
                    </tr>`;
            console.log(subThema);
        }

        if (output) {
            output.innerHTML += bodyData;
        }

        const deleteIcons = document.querySelectorAll(".deleteIcon");

        deleteIcons.forEach(function(this: HTMLElement) {
            this.addEventListener("click", function() {
                let subThemaId = this.getAttribute("data-id");
                if (subThemaId) {
                    deleteSubtheme(subThemaId);
                }
            });
        });
    } catch (error) {
        console.error(error);
    }
}
fillSubthemesTable();
