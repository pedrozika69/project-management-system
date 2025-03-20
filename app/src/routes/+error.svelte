<script lang="ts">
    // import { page } from '$app/state';
    import Footer from '$lib/components/Footer.svelte';
    import Fa from 'svelte-fa'

    import { faArrowLeft } from '@fortawesome/free-solid-svg-icons'

</script>

<div id="layoutError">
    <div id="layoutError_content">
        <main>
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-6">
                        <div class="text-center mt-4">
                            <img class="mb-4 img-error" src="/img/error-404-monochrome.svg" alt="Not Found" />
                            <p class="lead">This requested URL was not found on this server.</p>
                            <a href="/">
                                <Fa icon={faArrowLeft} />
                                Return to Home
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>
    <div id="layoutError_footer">
        <Footer />
    </div>
</div>
<!-- <h1>{page.status}: {page.error.message}</h1> -->