export function initFileDropZone(dropZoneElement, inputFileContainer) {
    const inputFile = inputFileContainer.querySelector("input");

    function onDragOver(e) {
        e.preventDefault();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");
    }

    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");

        inputFile.files = e.dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    // Register functions
    dropZoneElement.addEventListener("dragenter", onDragOver);
    dropZoneElement.addEventListener("dragover", onDragOver);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
    dropZoneElement.addEventListener("pqste", onPaste);

    // Allows to unregister  events along with Blazor component destruction.
    return {
        dipose: () => {
            dropZoneElement.removeEventListener("dragenter", onDragOver);
            dropZoneElement.removeEventListener("dragover", onDragOver);
            dropZoneElement.removeEventListener("dragleave", onDragLeave);
            dropZoneElement.removeEventListener("drop", onDrop);
            dropZoneElement.removeEventListener("pqste", onPaste);
        }
    }

}